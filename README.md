# Fluffy-Run

This game was developed as part of the tutorial project for **Team 3** at _CIEN_, the game development club in Chung-Ang University.

<img src="./Docs/img/Title.png" alt="타이틀" width="70%">
<img src="./Docs/img/StageSelection.png" alt="스테이지 선택" width="70%">
<img src="./Docs/img/stage1.png" alt="스테이지1" width="70%">
<img src="./Docs/img/screenshot.png" alt="스테이지 선택 및 플레이 화면 스크린샷" width="70%">

## Introduction

### Development Environment

Unity 2021.3.22f1

#### Game Genre

Puzzle Platform & Stage Format

#### Objective

Provide players with fun and a sense of accomplishment.

#### Core mechanic

The core mechanic revolves around designing various stages with unique configurations and providing players with feedback based on their performance.

#### Game Loop

Clear each stage within a given time limit.
<br><br/>

## Features

<img src="./Docs/img/short_intro.png" alt="전체 기능 요약" width="80%">

### 1. Player Movement

- Move Left/Right: Arrow Keys (⬅️➡️)
- Jump: `Space Bar`

### 2. Main Interactions

#### Levers

- When the player comes into contact with a lever and presses the ⬆️ arrow key, the lever's ON/OFF state toggles, and the connected platform object moves.

#### Buttons

- When the player or a block touches a button, the connected platform object moves while the button is held down.
- Once released, the platform object returns to its original state.

#### Movable Blocks

- The player can push the blocks to perform necessary actions to clear the game.
  <br><br/>

## Developers

- [Jimin Kim](https://github.com/kjimin0619)
- [Jihun Kim](https://github.com/AppliedAlpha)
- [Minseok Gu](https://github.com/Evturtl)
  <br><br/>

## How to play

#### Step 1. Go to '[Releases](https://github.com/kjimin0619/Fluffy-Run/releases)' of this repository.

#### Step 2. In Assets, download `Fluffy-Run-(BUILD_VERSION).zip` file.

#### Step 3. Extract zip file, and run `Fluffy Run.exe`.

#### Step 4. Enjoy it as much as you want!

## Target Platform
Windows x64

## Dependency

#### 본 게임은 Unity 무료 에셋을 활용하여 제작되었습니다.

https://assetstore.unity.com/packages/2d/environments/free-platform-game-assets-85838
