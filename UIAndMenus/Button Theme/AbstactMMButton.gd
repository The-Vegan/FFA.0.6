extends Button

var mainMenu
var destination : int

signal gamemode

const MAINMENU = 0
const SOLO = 1
const CHARACTERSELECT = 2
const LEVELSELECT = 3

var mode = 0
var character = 0


func _ready():
	mainMenu = get_parent().get_parent()
	$AnimatedSprite.play("Normal")
	connect("gamemode",mainMenu,"SetGame")

#YOU NEED TO OVERRIDE THE DESINATION IN THE READY OVERRIDE
func _pressed():
	mainMenu.MoveCameraTo(destination)
	emit_signal("gamemode",mode,character)

func _notification(what):
	match(what):
		NOTIFICATION_MOUSE_ENTER:
			$AnimatedSprite.play("Hovered")
		NOTIFICATION_MOUSE_EXIT:
			$AnimatedSprite.play("Normal")

