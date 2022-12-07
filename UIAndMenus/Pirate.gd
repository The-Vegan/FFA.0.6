extends "res://UIAndMenus/Button Theme/AbstactMMButton.gd"

func _ready():
	._ready()
	destination = LEVELSELECT
	mode = mainMenu.gameMode
	character = 1
	
