from flask import Flask, request

app = Flask(__name__)

move1 = "-1"
move2 = "-1"



@app.route("/")
def hello_world():
    return "<p>Landing page</p>"

@app.route("/send_result", methods=['POST'])
def send_result():
    global move1
    global move2
    global player1
    global player2
    if request.method == 'POST':
        if move1 == "-1":
            move1 = request.form.get('move')  
            return "1"
        elif move2 == "-1":
            move2 = request.form.get('move')
            return "2"
        return "-1"

@app.route("/get_result", methods=['POST'])
def get_result():
    global move1
    global move2
    global player1
    global player2
    if request.method == 'POST':
        move1temp = move1
        move2temp = move2

        player = request.form.get('player_number')

        if player == "1":
            if move2 != "-1":
                move2 = "-1"
                print(move2temp)
                return str(move2temp)
        elif player == "2":
            if move1 != "-1":
                move1 = "-1"
                print(move1temp)
                return str(move1temp)

        return '-1'

