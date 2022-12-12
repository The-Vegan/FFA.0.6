extends Control


var camera : Camera2D

var gameMode: int
var playerCharacter: int
var team :int = 0


#Camera positions for menus
var back = Vector2(0,0)
const MAINMENU = Vector2(0,0)
const SOLO = Vector2(0,-576)
const CHARSELECT = Vector2(-1024,0)
const LEVELSELECT = Vector2(-2048,0)




func _ready():
	camera = $Camera2D
	pass # Replace with function body.

func MoveCameraTo(var destination : int):
	
	match(camera.position):#Tells where to go if back is pressed
		SOLO:
			back = 1
		CHARSELECT:
			back = 2
		_:#default goes back to mainMenu /!\ DANGEROUS
			back = 0
	
	
	
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
	

func SetGame(mode : int, character : int):
	
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
		1:
			print("Pirate")
		2:
			print("Blahaj")
	
	
	pass

func DisplayErr():
	var tween = $Error/Tween
	
	$Error.modulate.a = 1
	yield(get_tree().create_timer(2),"timeout")
	tween.interpolate_property($Error.modulate,"a",1,0,3,Tween.TRANS_SINE,Tween.EASE_IN)
	
	
	


