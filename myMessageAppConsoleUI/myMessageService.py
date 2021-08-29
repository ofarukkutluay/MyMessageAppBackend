import requests


apiUrl = 'https://api.kutluay.net/'
# adminUser = {"email": "admin@admin.com", "password": "string"}


def login(email, password):
    headers = {'Content-Type': 'application/json'}
    user = {'email': email, 'password': password}
    r = requests.post(apiUrl+'api/Auth/Login', headers=headers, json=user)
    return r


def getPersonByEmail(email, token):
    authHeaders = {'Content-Type': 'application/json',
                   'Authorization': 'Bearer '+token}
    reciverUser = requests.get(
        apiUrl+'api/Users/GetPersonByEmail', headers=authHeaders, params={'email': email})
    return reciverUser


def getBySenderAndReciverAll(senderId, reciverId, token):
    authHeaders = {'Content-Type': 'application/json',
                   'Authorization': 'Bearer '+token}
    all_messages = requests.get(
        apiUrl+'api/Messages/GetBySenderAndReciverAll', headers=authHeaders, params={'senderId': senderId, 'reciverId': reciverId})
    return all_messages


def sendMessage(senderId, reciverId, message, token):
    authHeaders = {'Content-Type': 'application/json',
                   'Authorization': 'Bearer '+token}
    sendMessage = {
        "senderUserId": senderId,
        "reciverUserId": reciverId,
        "text": message
    }
    send = requests.post(
        apiUrl+'api/Messages/Add', headers=authHeaders, json=sendMessage)
    return send
