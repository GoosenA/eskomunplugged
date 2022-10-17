import base64
import os
import json

from flask import Flask, request
from google.cloud import pubsub_v1
from google.cloud import firestore

from eskom_se_push import EskomSePushAPI

app = Flask(__name__)

@app.route("/update", methods=['POST'])
def process_message():
    try:
        read_secret_value()
        print(f'DBG: len(SECRET_VALUE): {len(os.environ["SECRET_VALUE"])}')
        print(f'DBG: GOOGLE_CLOUD_PROJECT: {os.environ["GOOGLE_CLOUD_PROJECT"]}')
        print(f'DBG: PUBSUB_TOPIC: {os.environ["PUBSUB_TOPIC"]}')
        envelope = json.loads(request.data)
        payload = base64.b64decode(envelope['message']['data']).decode('UTF-8')
    except Exception as exception:
        return f'Bad Request: {exception}', 400

    if payload == 'all':
        print("TODO: Get list of all users")
        dummy_list = ["user_1", "user_2"]

        for user_id in dummy_list:
            print(f"Publishing user ID: {user_id}")
            publisher = pubsub_v1.PublisherClient()
            topic_path = publisher.topic_path(os.environ['GOOGLE_CLOUD_PROJECT'],
                                            os.environ['PUBSUB_TOPIC'])
            future = publisher.publish(topic_path, b'{user_id}')
            future.result()

    else:
        print(f"Call function that updates user with ID: {payload}")
        update_stage_info(payload)
        print(f"Done processing updates for user with ID: {payload}")

    return f"OK - {payload}", 200

def read_secret_value():
    if "SECRET_VALUE" in os.environ and len(os.environ['SECRET_VALUE']) > 0:
        return
    secret_file = os.environ['SECRET_FILE']
    with open(secret_file) as file:
        os.environ['SECRET_VALUE'] = file.read()


def update_stage_info(user_id):
    #get stage info for user area
    token = os.environ("SECRET_VALUE")
    esp = EskomSePushAPI(token)

    # get user form database
    collection = "data"

    user = get_user_from_database(collection, user_id)
    user_area = user["area"]

    stages_info = esp.area_information(user_area)

    # format stages
    stages = format_user_stage_info(stages_info["schedule"])
    user["schedule"] = stages
    user_doc_id = user.pop("doc_id")
    # save stages info to user doc
    save_user_to_db(collection, user, user_doc_id)


def get_user_from_database(collection, user_id):
    db = firestore.Client()
    user_ref = db.collection(collection)
    docs = user_ref.where(u'user_id', u'==', user_id).stream()

    users = []
    for doc in docs:
        user = doc.to_dict()
        user["doc_id"] = doc.id
        users.append(user)
    
    return users[0]


def format_user_stage_info(stages_info):
    formatted_stages = {}

    for day, day_info in stages_info["days"].items():
        formatted_stages_info = []

        date = day_info["date"]
        name = day_info["name"]
        stages_info = day_info["stages"]

        for i, stage in enumerate(stages_info):
            formatted_stage = []
            for loadshedding_time in stage:
                split = loadshedding_time.split("-")
                times = {
                    "start": split[0],
                    "end": split[1]
                }
                
                formatted_stage.append(times)
            formatted_stages_info[i] = formatted_stage

        formatted_stages[date] = formatted_stages_info
    return formatted_stages


def save_user_to_db(collection, user_doc, user_doc_id=None):
    db = firestore.Client()

    if user_doc_id:
        db.collection(collection).document(user_doc_id).set(user_doc)
    else:
        db.collection(collection).add(user_doc)



if __name__ == "__main__":
    app.run(debug=True, host="0.0.0.0", port=int(os.environ.get("PORT", 8080)))
