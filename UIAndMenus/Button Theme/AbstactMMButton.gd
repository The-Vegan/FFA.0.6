extends Button

var mainMenu
var destination : int

const MAINMENU = 0
const SOLO = 1
const CLASSIC = 2
const CTF = 3
const CAMPAIGN = 4
const CHARACTERSELECT = 5
const LEVELSELECT = 6




func _ready():
	mainMenu = get_parent().get_parent()
	$AnimatedSprite.play("Normal")

#YOU NEED TO OVERRIDE THE DESINATION IN THE READY OVERRIDE
func _pressed():
	mainMenu.MoveCameraTo(destination)

func _notification(what):
	match(what):
		NOTIFICATION_MOUSE_ENTER:
			$AnimatedSprite.play("Hovered")
		NOTIFICATION_MOUSE_EXIT:
			$AnimatedSprite.play("Normal")

