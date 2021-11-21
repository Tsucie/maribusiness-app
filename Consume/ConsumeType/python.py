import urllib.request
import json
import os
import ssl

def allowSelfSignedHttps(allowed):
    # bypass the server certificate verification on client side
    if allowed and not os.environ.get('PYTHONHTTPSVERIFY', '') and getattr(ssl, '_create_unverified_context', None):
        ssl._create_default_https_context = ssl._create_unverified_context

allowSelfSignedHttps(True) # this line is needed if you use self-signed certificate in your scoring service.

# Request data goes here
data = {
    "Inputs": {
        "WebServiceInput0":
        [
            {
                'id': "2525363215",
                'date': "20201013T000000",
                'price': "356300",
                'bedrooms': "3",
                'bathrooms': "1",
                'sqft_living': "1180",
                'sqft_lot': "5650",
                'floors': "1",
                'waterfront': "0",
                'view': "0",
                'condition': "3",
                'grade': "7",
                'sqft_above': "1180",
                'sqft_basement': "0",
                'yr_built': "1955",
                'yr_renovated': "0",
                'zipcode': "98178",
                'lat': "475.11199999999997",
                'long': "-122.257",
                'sqft_living15': "1340",
                'sqft_lot15': "5650",
            },
        ],
    },
    "GlobalParameters": {
    }
}

body = str.encode(json.dumps(data))

url = 'http://630317bb-4577-4216-82a1-5e4fe48d92db.southeastasia.azurecontainer.io/score'
api_key = 'LHnJg8Z8wdiyboSs38cXXz7w9nbgWCx2' # Replace this with the API key for the web service
headers = {'Content-Type':'application/json', 'Authorization':('Bearer '+ api_key)}

req = urllib.request.Request(url, body, headers)

try:
    response = urllib.request.urlopen(req)

    result = response.read()
    print(result)
except urllib.error.HTTPError as error:
    print("The request failed with status code: " + str(error.code))

    # Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
    print(error.info())
    print(json.loads(error.read().decode("utf8", 'ignore')))