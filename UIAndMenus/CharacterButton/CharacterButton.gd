extends Button

signal setChar

onready var mainMenu = get_parent().get_parent()
var charID : int = 0

func _ready():
	connect("setChar",mainMenu,"setCharacter")


func _pressed():
	emit_signal("setChar",charID)
