extends Button

var loadedLevel
export var lvlToLoad : PackedScene
onready var mainMenu = get_parent().get_parent()

func _ready():
	
	
	
	pass # Replace with function body.

func _pressed():
	GenerateLevel(mainMenu.playerCharacter,mainMenu.gameMode)

func GenerateLevel(character : int , mode : int):
	loadedLevel = lvlToLoad.instance()
	
	loadedLevel.connect("loadComplete",self,"LevelLoaded")
	
	loadedLevel.InitPlayerAndMode(character,mode,1,mainMenu.team,mainMenu.waitForMultiplayer)
	
	

func LevelLoaded(success : bool):
	print("LoadCompleted In MainMenu")
	
	# This method is accesed via signal from level
	if(success):
		get_tree().root.add_child(loadedLevel)
		mainMenu.queue_free()
	else:
		print("ERROR")
	pass
