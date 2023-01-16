# Vars-Adventure
AITU Software Design Pattern project.
A little game I continue working on as a way to study and practice software design patterns with Unity Engine. 

So far 5 design patterns were implemented into the development process:

## STRATEGY PATTERN
Behavioral design pattern that lets you define a family of algorithms, put each of them into a separate class, and make their objects interchangeable.

Was used in the process of determining visibility of the player. My game is using tiles to represent and draw the environment of a video game level. The problem is that there are multiple types of different tiles that I am using, which means that there will be a need to use different algorithms.

## OBSERVER PATTERN
Behavioral design pattern that lets you define a subscription mechanism to notify multiple objects about any events that happen to the object they’re observing.

Was used in the event system of the game. It is important to keep animation and logic of the game separate. Observer pattern allows me to achieve this by introducing a "mediator" class, which reacts to certain events in the game.

## DECORATOR PATTERN
Structural design pattern that lets you attach new behaviors to objects by placing these objects inside special wrapper objects that contain the behaviors.

Was used in the inventory system of the game. Almost every RPG has combat and mine is not an exception. To implement combat mechanics into the game I used the decorator pattern to make sure that it would be easy to wrap weapon objects in buff objects.

## SINGLETON PATTERN
Creational design pattern that lets you ensure that a class has only one instance, while providing a global access point to this instance.

Was used to make sure that various manager objects could only exist once in the scene. My game requires there to be only one instance of a specific object which is responsible for various visual effects, such as sprite mask control, for example. Singleton allows us to ensure that there is only ever one static instance of a class running at all times. 

## COMMAND PATTERN
Behavioral design pattern that turns a request into a stand-alone object that contains all information about the request. 

Was used in the movement system of the player character. Player's game avatar should be capable of following player's commands. I implemented three different types of commands my character can follow: move, schedule move and attack.
