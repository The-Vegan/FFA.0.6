extends Button

var loadedLevel
export var lvlToLoad : PackedScene
onready var mainMenu = get_parent().get_parent()

func _ready():
	
	
	
	pass # Replace with function body.

func _pressed():
	loadedLevel = lvlToLoad.instance()
	
	loadedLevel.connect("loadComplete",self,"LevelLoaded")
	
	loadedLevel.InitPlayerAndMode(#The following are the arguments
	mainMenu.playerCharacter,
	mainMenu.gameMode,
	12,#number of players
	mainMenu.team,
	mainMenu.chosenTeam)



func LevelLoaded(success : bool):
	print("LoadCompleted In MainMenu")
	
	# This method is accesed via signal from level
	if(success):
		get_tree().root.add_child(loadedLevel)
		mainMenu.queue_free()
	else:
		print("ERROR")
	pass
