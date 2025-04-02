# Chest System - Unity Game Project

## Overview

The **Chest System** is a Unity-based game project that implements a chest unlocking mechanic commonly found in mobile games. Players can generate chests, unlock them over time or instantly with gems, collect rewards, and undo certain actions. The system is designed with scalability and maintainability in mind, using various design patterns to ensure clean and modular code.

This project was developed as a learning exercise to demonstrate proficiency in Unity, C#, and software design principles. It includes a fully functional chest management system with a user interface, timer mechanics, a queue system, a gem-based instant unlock feature, and an undo functionality.

---

## Features

- **Chest Generation and Slot Management**:
  - Generate chests with different types (e.g., Common, Rare, Epic, Legendary).
  - Manage up to 4 chest slots, with a popup notification when slots are full.

- **Timer System**:
  - Unlock chests over time with a countdown timer.
  - Only one chest can be unlocking at a time.
  - A queue system automatically starts the next chest’s timer when the current one finishes.

- **Reward Collection**:
  - Collect coins and gems as rewards when a chest’s timer reaches zero.
  - UI updates in real-time to reflect the player’s total coins and gems.

- **Gem System and Instant Unlocking**:
  - Instantly unlock chests using gems, with the cost calculated as 1 gem per minute of remaining time (rounded up).
  - Gem cost is displayed on the "Unlock with Gems" button.
  - Gems are deducted from the player’s total, with a popup if there are insufficient gems.

- **Undo Functionality**:
  - Undo the "Unlock with Gems" action, refunding the gems and restoring the chest’s timer and state.
  - The "Undo" button appears after using gems and disappears after collecting rewards or undoing.

- **User Interface**:
  - Clean UI with chest slots showing the chest sprite, timer, and action buttons ("Start Timer," "Unlock with Gems," "Collect," "Undo").
  - Popups for user feedback (e.g., "Slots Full," "Another chest is already unlocking").
  - Real-time updates for coins and gems in the UI.

---
## Installation and Setup

### Prerequisites
- **Unity**: Version 2021.3 or later (LTS recommended).
- **TextMeshPro**: Ensure TextMeshPro is imported into your Unity project (available via the Package Manager).

### Steps
1. **Clone the Repository**:
   

2. **Open in Unity**:
   - Open Unity Hub.
   - Click "Add" and select the cloned `chest-system` folder.
   - Open the project in Unity.

3. **Set Up the Scene**:
   - Open the main scene (e.g., `MainScene`) located in the `Assets/Scenes` folder.
   - Ensure all required GameObjects are in the scene:
     - `ChestSlotManager` (manages chest slots).
     - `TimerManager` (manages the timer system).
     - `CommandManager` (manages undo functionality).
     - `PlayerData` (manages coins and gems).
     - `UIManager` (manages UI updates for coins and gems).
     - `PopupPanel` and `TimerActivePopupPanel` (for popup notifications).
     - A Canvas with UI elements for chest slots, coins, gems, and popups.

4. **Assign References**:
   - In the `ChestSlotManager` GameObject, ensure the following are assigned in the Inspector:
     - `ChestTypes`: Array of `ChestType` ScriptableObjects.
     - `ChestSlotUIs`: Array of `ChestSlotUI` components for each slot.
     - `SlotsFullPopup`: The "PopupPanel" GameObject.
     - `TimerActivePopup`: The "TimerActivePopupPanel" GameObject.
   - In the `UIManager` GameObject, assign the `CoinsText` and `GemsText` fields to the respective UI text elements.
   - In the `Popup` components (on "PopupPanel" and "TimerActivePopupPanel"), assign the `MessageText` and `CloseButton` fields.

5. **Play the Scene**:
   - Press the Play button in Unity to test the game.

---

## Usage

1. **Generate a Chest**:
   - Click the "Generate Chest" button to add a chest to an empty slot.
   - If all slots are full, a "Slots Full" popup will appear.

2. **Start the Timer**:
   - Click the "Start Timer" button on a chest to begin unlocking it.
   - Only one chest can be unlocking at a time. If another chest is already unlocking, a popup will appear, and the new chest will be added to the queue.

3. **Instant Unlock with Gems**:
   - While a chest is unlocking, click the "Unlock with Gems" button to instantly unlock it.
   - The gem cost is displayed on the button (1 gem per minute of remaining time, rounded up).
   - If you have enough gems, the chest will unlock immediately, and an "Undo" button will appear.

4. **Undo the Unlock**:
   - Click the "Undo" button to refund the gems and restore the chest’s timer and state.
   - The "Undo" button disappears after undoing or collecting rewards.

5. **Collect Rewards**:
   - When a chest’s timer reaches zero, click the "Collect" button to receive coins and gems.
   - The UI updates to reflect the new totals for coins and gems.

---

## Project Structure

- **Assets/Scripts**:
  - `Chest.cs`: Core chest logic with state management.
  - `ChestType.cs`: ScriptableObject for defining chest types (e.g., timer, rewards, sprite).
  - `ChestSlotManager.cs`: Manages chest slots and the queue system.
  - `ChestSlotUI.cs`: UI logic for each chest slot.
  - `ChestController.cs`: Controller for handling chest actions (MVC pattern).
  - `TimerManager.cs`: Manages the timer system and ensures only one chest unlocks at a time.
  - `PlayerData.cs`: Manages the player’s coins and gems, with an event for UI updates.
  - `UIManager.cs`: Updates the UI for coins and gems.
  - `Popup.cs`: Generic popup system for user notifications.
  - `CommandManager.cs`: Manages undo functionality using the Command Pattern.
  - `States/`:
    - `IChestState.cs`: Interface for chest states.
    - `LockedState.cs`, `UnlockingState.cs`, `CollectedState.cs`: State implementations.
  - `Commands/`:
    - `ICommand.cs`: Interface for commands.
    - `StartTimerCommand.cs`, `UnlockWithGemsCommand.cs`: Command implementations.

- **Assets/Scenes**:
  - `MainScene`: The main game scene with all GameObjects set up.

- **Assets/Resources**:
  - `ChestTypes/`: Folder containing `ChestType` ScriptableObjects for different chest types.

---

## Design Patterns Used

This project leverages several design patterns to ensure clean, modular, and scalable code:

- **Model-View-Controller (MVC)**:
  - **Model**: `Chest`, `PlayerData` (data and logic).
  - **View**: `ChestSlotUI`, `UIManager` (UI rendering).
  - **Controller**: `ChestController` (handles user input and updates the model).

- **State Pattern**:
  - Used to manage the chest’s state (`LockedState`, `UnlockingState`, `CollectedState`).
  - Each state handles specific behavior (e.g., starting the timer, unlocking with gems).

- **Observer Pattern**:
  - `Chest.OnStateChanged`: Notifies the UI when the chest’s state or timer changes.
  - `PlayerData.OnPlayerDataChanged`: Notifies the UI when coins or gems change.

- **Command Pattern**:
  - Used for the undo functionality (`StartTimerCommand`, `UnlockWithGemsCommand`).
  - `CommandManager` stores a history of commands and allows undoing actions.

- **Singleton Pattern**:
  - `ChestSlotManager`, `TimerManager`, `CommandManager`, `PlayerData`, `UIManager`: Ensures global access to these managers.

- **Queue Pattern**:
  - A `Queue<Chest>` in `ChestSlotManager` manages chests waiting to be unlocked.

---

## Future Improvements

- **Save/Load System**:
  - Add saving and loading of chest states, player data, and queue using PlayerPrefs or a file system.

- **Animations and Sound**:
  - Add animations for chest unlocking and reward collection.
  - Include sound effects for actions like generating a chest, starting a timer, and collecting rewards.

- **Main Menu**:
  - Create a main menu with options to start the game, view settings, or exit.

- **Chest Rarity Effects**:
  - Add visual effects or different UI styles based on chest rarity (e.g., glowing effects for Legendary chests).

- **Localization**:
  - Support multiple languages for UI text and popups.

---

## Contributing

Contributions are welcome! If you’d like to contribute:
1. Fork the repository.
2. Create a new branch (`git checkout -b feature/your-feature`).
3. Make your changes and commit them (`git commit -m "Add your feature"`).
4. Push to your branch (`git push origin feature/your-feature`).
5. Open a Pull Request.

