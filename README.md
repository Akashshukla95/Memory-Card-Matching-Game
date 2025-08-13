# Unity Memory Card Matching Game

A simple memory card matching game built in Unity with the following core features:
1. Card Flip Logic
2. Scoring System
3. Save/Load Progress
4. Sound Effects

---

## Features

### 1. Card Flip Logic
- Each card can be flipped between **face-up** and **face-down** states.
- Matching cards stay face-up, mismatched cards flip back after a short delay.
- Implemented in `cardScript.cs`.

### 2. Scoring System
- Players earn points for correct matches and lose points for mismatches.
- Score is displayed in real-time on the UI.
- Implemented in `gameManager.cs` with `scoreText` updates.

### 3. Save/Load Progress
- The game saves:
  - Current score
  - Remaining matches
- Uses Unity’s `PlayerPrefs` for simple persistence.
- On restart, progress is restored so players can continue where they left off.

### 4. Sound Effects
- Audio cues for:
  - Card Flip
  - Match Found
  - Mismatch
  - Game Over
- Sounds are assigned via the Unity Inspector and played using `AudioSource`.

---

## How to Run

1. Open the project in Unity (6000.0.48 or newer recommended).
2. Open `menuScene.unity` or `gameScene.unity` from `Assets/Scenes`.
3. Press Play in the Unity Editor.
4. Use your mouse to flip cards and match pairs.
