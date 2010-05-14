import smtplib
import sys
import os
smtpserver = "outbound.mailhop.org"
AUTHREQUIRED = 1 # if you need to use SMTP AUTH set to 1
f = open("..\\App_Data\\tmp.txt", "r")
lines = f.readlines()
f.close()
smtpuser = lines[0]
smtpuser = smtpuser.strip()
smtppass = lines[1]
smtppass = smtppass.strip()
from_address = lines[2]
from_address = from_address.strip()
to_address = lines[3]
to_address = to_address.strip()
subject = lines[4]
subject = subject.strip()
content_type = lines[5]
content_type.strip()
body = lines[6 : ]
RECIPIENTS = to_address
SENDER = from_address
msglines = body#sys.stdin.readlines()
msg = ''
for line in msglines:
    msg = msg + line
print 'smtpuser:%s' % (smtpuser)
print 'smtppass: %s' % (smtppass)
print 'recipient: %s' % (RECIPIENTS)
print 'sender : %s' % (SENDER)
print 'subject : %s' % (subject)
print 'msg: %s' % (msg)

session = smtplib.SMTP(smtpserver)
if AUTHREQUIRED:
    session.login(smtpuser, smtppass)

headers = "From: %s\r\nTo: %s\r\nSubject: %s\r\nContent-Type: %s\r\n\r\n" % (SENDER, RECIPIENTS, subject, content_type)
msg = headers + msg
smtpresult = session.sendmail(SENDER, RECIPIENTS, msg)
os.remove("..\\App_Data\\tmp.txt")