import smtplib
import sys
smtpserver = "outbound.mailhop.org"
AUTHREQUIRED = 1 # if you need to use SMTP AUTH set to 1
args = {}
for arg in sys.argv:
    parts = arg.split(':')
    if len(parts) != 2:
        continue
    name = parts[0]
    value = parts[1]
    args[name] = value
        
        


smtpuser = args['username']
smtppass = args['password']
RECIPIENTS = args['recipient']
SENDER = args['sender']
subject = args['subject']
msglines = sys.stdin.readlines()
msg = ''
for line in msglines:
    msg = msg + line
#print 'smtpuser:%s' % (smtpuser)
#print 'smtppass: %s' % (smtppass)
#print 'recipient: %s' % (RECIPIENTS)
#print 'sender : %s' % (SENDER)
#print 'subject : %s' % (subject)
#print 'msg: %s' % (msg)

session = smtplib.SMTP(smtpserver)
if AUTHREQUIRED:
    session.login(smtpuser, smtppass)
headers = "From: %s\r\nTo: %s\r\nSubject: %s\r\n\r\n" % (SENDER, RECIPIENTS, subject)
msg = headers + msg
smtpresult = session.sendmail(SENDER, RECIPIENTS, msg)
#if smtpresult:
#    errstr = ""
#    for recip in smtpresult.keys():
#        errstr = """Could not deliver mail to: %s Server said: %s, %s, %s""" % ( recip, smtpresult[recip][0],
#                                                                                 smtpresult[recip][1], errstr)
#        raise smtplib.SMTPException, errstr
