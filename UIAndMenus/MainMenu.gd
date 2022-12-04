extends Control


var camera : Camera2D

#Camera positions for menus
const MAINMENU = Vector2(0,0)
const SOLO = Vector2(0,-576)






func _ready():
	camera = $Camera2D
	pass # Replace with function body.

func MoveCameraTo(var destination : int):
	
	match(destination):
		0:#main menu
			camera.position = MAINMENU
			print("to main menu")
		1:#solo
			camera.position = SOLO
			print("to solo")
		2:#classic
			print("to classic")
		3:#ctf
			print("to ctf")
		4:#campaign
			print("to campaign")
		5:#character selection
			pass
		6:#level selection
			pass
	




func _on_Back_pressed():
	pass # Replace with function body.
