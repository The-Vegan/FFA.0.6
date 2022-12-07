extends Control


var camera : Camera2D

var gameMode: int
var playerCharacter: int
var levelToLoad : int


#Camera positions for menus
const MAINMENU = Vector2(0,0)
const SOLO = Vector2(0,-576)
const CHARSELECT = Vector2(-1024,0)





func _ready():
	camera = $Camera2D
	pass # Replace with function body.

func MoveCameraTo(var destination : int):
	
	match(destination):
		0:#main menu
			camera.position = MAINMENU
			
		1:#solo
			camera.position = SOLO
		2:#classic
			camera.position = CHARSELECT
		3:#ctf
			camera.position = CHARSELECT
		4:#campaign
			pass
		5:#character selection
			pass
		6:#level selection
			pass
	

func setGame(mode : int, character : int , selectedLevel : int):
	
	match(mode):
		0:#None
			print("None")
		1:#Classic
			print("Classic")
		2:#CTF
			print("CTF")
		3:#Campaign
			print("Campaign")
		
	match(character):
		_:
			pass
	
	match(selectedLevel):
		_:
			pass
	
	
	pass
