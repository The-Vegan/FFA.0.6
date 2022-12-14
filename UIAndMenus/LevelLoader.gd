extends Button

export var lvlToLoad : PackedScene
onready var mainMenu = get_parent().get_parent()

func _ready():
	pass # Replace with function body.

func _pressed():
	#checks for errors
	if ((mainMenu.gameMode == 0)||(mainMenu.playerCharacter == 0)):
		mainMenu.MoveCameraTo(Vector2(0,0))
		mainMenu.DisplayErr()
		return
	
	GenerateLevel(lvlToLoad,mainMenu.playerCharacter,mainMenu.gameMode)

func GenerateLevel(lvlScene : PackedScene, character : int , mode : int):
	var loadedLevel = lvlScene.instance()
	
	loadedLevel.InitPlayerAndMode(character,mode,mainMenu.team,mainMenu.waitForMultiplayer)
	
	get_tree().root.add_child(loadedLevel)
	
	mainMenu.queue_free()