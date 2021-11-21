library("RCurl")
library("rjson")

# Accept SSL certificates issued by public Certificate Authorities
options(RCurlOptions = list(cainfo = system.file("CurlSSL", "cacert.pem", package = "RCurl"), ssl.verifypeer = FALSE))

h = basicTextGatherer()
hdr = basicHeaderGatherer()

# Request data goes here
req = list(
    Inputs = list(
        "WebServiceInput0" = list(
            list(
                'id' = "2525363215",
                'date' = "20201013T000000",
                'price' = "356300",
                'bedrooms' = "3",
                'bathrooms' = "1",
                'sqft_living' = "1180",
                'sqft_lot' = "5650",
                'floors' = "1",
                'waterfront' = "0",
                'view' = "0",
                'condition' = "3",
                'grade' = "7",
                'sqft_above' = "1180",
                'sqft_basement' = "0",
                'yr_built' = "1955",
                'yr_renovated' = "0",
                'zipcode' = "98178",
                'lat' = "475.11199999999997",
                'long' = "-122.257",
                'sqft_living15' = "1340",
                'sqft_lot15' = "5650"
            )
        )
    ),
    GlobalParameters = setNames(fromJSON('{}'), character(0))
)

body = enc2utf8(toJSON(req))
api_key = "LHnJg8Z8wdiyboSs38cXXz7w9nbgWCx2" # Replace this with the API key for the web service
authz_hdr = paste('Bearer', api_key, sep=' ')

h$reset()
curlPerform(
    url = "http://630317bb-4577-4216-82a1-5e4fe48d92db.southeastasia.azurecontainer.io/score",
    httpheader=c('Content-Type' = "application/json", 'Authorization' = authz_hdr),
    postfields=body,
    writefunction = h$update,
    headerfunction = hdr$update,
    verbose = TRUE
)

headers = hdr$value()
httpStatus = headers["status"]
if (httpStatus >= 400)
{
    print(paste("The request failed with status code:", httpStatus, sep=" "))

    # Print the headers - they include the request ID and the timestamp, which are useful for debugging the failure
    print(headers)
}

print("Result:")
result = h$value()
print(fromJSON(result))