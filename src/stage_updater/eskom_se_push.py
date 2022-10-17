import requests

class EskomSePushAPI:
    base_url = "https://developer.sepush.co.za/business/2.0"
    token = None
    
    def __init__(self, token):
        self.token = token
    

    def status(self, params=None, headers={}):
        url = self.base_url + "/status"
        if not headers or "Token" not in headers:
            headers["Token"] = self.token
        return requests.get(url, headers=headers, params=params)

    def allowance(self, params=None, headers={}):
        url = self.base_url + "/api_allowance"
        if not headers or "Token" not in headers:
            headers["Token"] = self.token
        return requests.get(url, headers=headers, params=params)
        

    def area_information(self, params=None, headers={}):
        url = self.base_url + "/area"
        if not headers or "Token" not in headers:
            headers["Token"] = self.token
        return requests.get(url, headers=headers, params=params)


    def area_information_gps(self, params=None, headers={}):
        url = self.base_url + "/areas_nearby"
        if not headers or "Token" not in headers:
            headers["Token"] = self.token
        return requests.get(url, headers=headers, params=params)


    def area_search(self, params=None, headers={}):
        url = self.base_url + "/areas_search"
        if not headers or "Token" not in headers:
            headers["Token"] = self.token
        return requests.get(url, headers=headers, params=params)
        

    def topics_nearby(self, params=None, headers={}):
        url = self.base_url + "/topics_nearby"
        
        if not headers or "Token" not in headers:
            headers["Token"] = self.token
        
        if not "lat" in params or not "lon" in params:
            raise ValueError("Missing parameters")
        return requests.get(url, headers=headers, params=params)        

