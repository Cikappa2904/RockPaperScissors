from flask import Flask, request

app = Flask(__name__)

x = 0
y = 0

@app.route("/")
def hello_world():
    return "<p>Landing page</p>"

@app.route("/test")
def test():
    global x
    global y
    
    if x==0:
        x = 1
        return str(x)
    else:
        if y==0:
            y = 2
            return str(y)  

@app.route("/test1", methods=['POST'])
def test1():
    if request.method == 'POST':
        thing1 = request.form.get('thing1')
        thing2 = request.form.get('thing2')
        return thing1

    
    