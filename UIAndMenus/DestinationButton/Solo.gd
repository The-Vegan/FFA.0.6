extends "res://UIAndMenus/DestinationButton/DestinationButton.gd"

func _ready():
	#._ready()
	destination = SOLO
	mainMenu.waitForMultiplayer = false
