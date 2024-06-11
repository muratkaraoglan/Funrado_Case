# Funrado_Case

You are responsible for designing a clone of 3D Puzzle game called “Frog Feed Order” with Unity 2022 LTS. 

1. You should use all concepts properly which are listed in “Design Expectations” section while implementing the 
project.
2. There should be a map made from square nodes with cells inside. (There are hex nodes on reference, we use 
square nodes here)
3. Each cell can contain a “Frog”, a “Arrow” or a “Berry”. Each of them must have 4 different color variations. The 
aim is the feeding all the frogs by clicking the frogs in correct order.
4. Frogs and arrows can face up, down, left or right. If they are placed on border cells, they shouldn’t face outside 
of the map.
5. Players can only interact with the frogs by right-clicking them. When frog is triggered by player, it attempts to 
eat berries by moving its tongue to the faced direction. You should prepare proper tongue visual yourself.
6. If moving tongue touches same-colored berries with the frog, it continues its movement until it reaches the 
last cell on its way. After that, tongue returns to the frog while collecting the berries. Finally, frog eats the 
berries arrive.
7. If tongue touches a wrong-colored berry or any other element before it reaches final cell, tongue returns 
without collecting any berries.
8. Each node must be able to contain multiple cells and always the content of the most up cell must be activated. 
(E.g. Let’s assume that we have a cell with red berry, a cell with yellow berry and a cell with green berry from 
bottom to top in the same node. Green berry must be activated at the beginning. (We should see all cells but 
only see green berry)
9. If tongue movement started successfully and collecting phase started, top cell should be disappeared and 
bottom one should be activated with its content just after the tongue leaves that cell. Finally, frog and its cell 
should be also deactivated after frog eats the berries. (Please check reference video for demonstration.)
10. If tongue reaches same colored arrow, it changes its direction to the same as the arrow’s. 
11. When there are no frogs left (All of them deactivated because they successfully ate berries.) the player wins 
the level.
12. There should be “Move Limit”. Each interaction with a frog should cost 1 move. If the player has no move left 
before feeding all the frogs, the player should lose the game.
13. Use provided assets where applicable.
14. Main camera projection must be orthographic, and perspective must be set according to the reference.
15. You should be able to answer all project-related questions.
16. There should be at least three different levels. (You can copy levels from the reference.)
17. If you can’t understand the tasks perfectly, please feel free to ask.
#########################################################################
DESIGN EXPECTATIONS
• OOP
Polymorphism
Inheritance
• S-O-L-I-D
• Design Patterns
Singleton
Observer 
MVC (MVVM)
• Draw Call Optimization
• Coroutine
• Events
#########################################################################
We expect you to write clean and reusable code. Also, all aspects of your project will be considered including 
your folder organization and your level design.
#########################################################################
IMPORTANT LINKS
• Reference Game Gameplay Video Link:
Frog Feed Order - Gameplay (youtube.com)
• Reference Game GooglePlay Link:
https://play.google.com/store/apps/details?id=com.funrado.frogfeedorder&gl=TR
#########################################################################
SAVE & SEND!
You are expected to share your project via BitBucket or Github (send us an email that contains the link to the 
project – umutsahin@funrado.com).
