# eskomunplugged

## Description
The goal of this build was to develop a slack app that could be used to inform your colleagues when you have loadshedding - ideally by updating your Slack status when you have loadshedding.

We managed to develop a Slack app that allows a user to set their area by searching for their area (this uses the ESP area_search endpoint), or by searching using their GPS coordinates (this uses the ESP area_nearby endpoint).

The user loadshedding schedules can then be queried from the Slack bot to see when everyone in a workspace that has configured the Slack app, has loadshedding.

## Database
For the database we used Google Firestore to store Slack user IDs and their EskomSePush area information (area IS and stage information). Due to constraints of working with C#, the data is stored in the following format:
```
{
  "area": "test_user_area_id",
  "name": "test_user",
  "stage_one": [],
  "stage_two": ["08:00","10:00"],
  "stage_three": ["08:00","10:00"],
  "stage_four": ["08:00","10:00", "14:00", "16:00"],
  "stage_five": ["08:00","10:00", "14:00", "16:00"],
  "stage_six": ["08:00","10:00", "14:00", "16:00"],
  "stage_seven": ["08:00","10:00", "14:00", "16:00", "20:00", "22:00"],
  "stage_eight": ["08:00","10:00", "14:00", "16:00", "20:00", "22:00"],
  "team_id: "team_id",
  "user_id: "user_id"
}

```


## Slack app
TODO - JDM



## Stage updater backend

### Description
The stage updater updates a users loadshedding stage schedules and stores it in FireStore for the Slack app to use. The stage updater works by retrieving the area information for a user using the configured area_id for that user. The loadshedding stage information is retrieved using the /area_information endpoint and formatted and stored in FireStore.

```mermaid
flowchart LR
  Scheduler -->|all| Topic[PubSub topic]
  subgraph PubSub
    Topic --> Push[Push message]
  end 
  subgraph CloudRun["Stage updater service"]
    Push-->all{If message = all}
    all-->|Yes| foreach[Foreach user in DB]
    all-->|No| update_stages[Update user LS schedule]
    foreach-->|user_id| Topic
  end
```

### Tools used
For this part of the project we used:
- Python3 and various libraries like 
    - Flask
    - GCP dependencies
    - Our own ESP wrapper for Python
- Google Cloud Run
- Google Cloud Build
- Google Cloud Secret Manager
- Google Cloud Scheduler
- Google Cloud PubSub


## Improvements
For this project the focus was on delivering a minimum viable product for the Offerzen ESP build demo. As such, there are many improvements that can be made to this build.
- Feature: Add Slack status updates for users
- Bug: Fix Gooogle Code build deployment error (No module named 'main')
- Task: Overall improvements to code quality
