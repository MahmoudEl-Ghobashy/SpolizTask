# 🏎️ Photon Reconnection Task  

📹 **For a quick video demonstration, go to:**  
📂 `Video/Spoilz test.mp4`  

---

## 🚀 How to Run the App  

### **1️⃣ Build the App**
- Open the project in **Unity**  
- Go to **File → Build Settings**  
- Select **Windows** as the platform  
- Click **Build and Run**  

### **2️⃣ Run Multiple Instances**
- Open **two instances** of the game  
- This allows testing **multiplayer features**  

### **3️⃣ Join or Create a Room**
- Press **Play** on both instances  
- The game will **automatically create or join a room**  

### **4️⃣ Test Reconnection**
- **Disconnect one player** (turn off WiFi or manually disconnect)  
- **Reconnect within the allowed time** to rejoin the same room  

---

## 🔄 Constraints for Reconnection  

✅ **Reconnection Attempts:**  
- The app will **attempt to reconnect 5 times**  

✅ **Time Limit for Each Attempt:**  
- Each reconnection attempt lasts **3 seconds**  

✅ **Successful Reconnection:**  
- If successful, the player **respawns at the exact position before disconnection**  

✅ **Failed Reconnection:**  
- If the room is **unavailable** or connection **is not restored**,  
  the player will be **sent back to the main menu** and **fully disconnected from Photon**  

---

🔥 **Now you're ready to test!** 🚗💨  
