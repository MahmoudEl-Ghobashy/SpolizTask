# SpolizTask
Photon reconnection task

for a quick video please go to Video/Spoilz test.mp4

To run the app: 
1- Build the app from unity to windows 
2- Run the app on 2 different instances 
3- Press play to create or join room 
4- Disconnect and reconnect within the dedicated time frame.

Constraints to reconnect:
1- After disconnection the app will attempts 5 times to reconnect to the same room 
2- Each attempt has a time frame of 3 seconds 
3- If successfully reconnected player will respawn to the same position as he was before disconnection 
4- In case of the room is not available or connection is not restored player will be taken back to the menu and disconnected completely from photon. 