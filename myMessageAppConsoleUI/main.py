import requests
import myMessageService


def getAllMessages(senderId,reciverId,token):
    for message in myMessageService.getBySenderAndReciverAll(senderId,reciverId,token).json()['data']:
        if message['senderUserId'] == senderId:
            print(f"Gönderilen -- {message['sendTime']}  -->  {message['text']}")
        if message['reciverUserId'] == senderId:
            print(f"{reciverId} gelen -- {message['sendTime']}  -->  {message['text']}")

while True:
    print('Giriş için;')
    loginEmail = input('Email : ')
    loginPassword = input('Password : ')
    loginUser = myMessageService.login(loginEmail, loginPassword)

    if loginUser.status_code == requests.codes.ok:
        print("Giriş başarılı!\n  ----------------------")
        loginStatus = True
        token = loginUser.json()['token']

        getUser = myMessageService.getPersonByEmail(loginEmail, token)
        if getUser.status_code == requests.codes.ok:
            #print(getUser.json())
            senderId = getUser.json()['data']['id']
            break
    print('Giriş Hatalı! Tekrar Deneyiniz...\n ----------------------')

while True:
    reciverEmail = input('Mesaj gönderilecek mail adresi : ')
    reciverUser = myMessageService.getPersonByEmail(reciverEmail, token)
    if reciverUser.status_code == requests.codes.ok:
        print(reciverUser)
        reciverId = reciverUser.json()['data']['id']
        break
    print('Kullanıcı bulunamadı! Tekrar Deneyiniz... \n -------------------')



while True:
    getAllMessages(senderId,reciverId,token)
    print("-------------------------- \n Tüm mesajları görebilmek için Enter a basınız.")
    message = input('Mesajınız : ')
    if message != "":
        send = myMessageService.sendMessage(senderId,reciverId,message,token)
        print(send.json())

