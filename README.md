# Slay the Spire 2 Multiplay Console Alert

A mod for Slay the Spire 2 that notifies you when another player (the host) executes a developer console command during multiplayer.

## Features

- **Block local console**: Prevents the dev console from opening via toggle keys such as `` ` `` or `'`.
- **Host command alert**: Displays an on-screen notification when the host executes a networked console command during multiplayer. The command itself is still allowed to run.

## Notification Example

```
[Console] Host executed a console command: gold add 9999
```

The notification disappears automatically after 3 seconds.

## Installation

1. Copy this mod into your STS2 mods folder.
2. Launch the game — the mod will load automatically.

## Build

```
dotnet build sts2-block-console.csproj
```

---

# Slay the Spire 2 Multiplay Console Alert

Slay the Spire 2 멀티플레이 중 다른 플레이어(호스트)가 개발자 콘솔 명령을 실행하면 화면에 알림을 표시하는 모드입니다.

## 기능

- **로컬 콘솔창 차단**: `` ` `` / `'` 등 토글 키로 개발자 콘솔이 열리지 않도록 막습니다.
- **호스트 명령 알림**: 멀티플레이 중 호스트가 네트워크 콘솔 명령을 실행하면 화면 좌측 하단에 알림을 표시합니다. 명령 실행 자체는 차단하지 않습니다.

## 알림 예시

```
[콘솔] 호스트가 콘솔 명령을 실행했습니다: gold add 9999
```

알림은 3초 후 자동으로 사라집니다.

## 설치

1. 이 모드를 STS2 모드 폴더에 복사합니다.
2. 게임을 실행하면 자동으로 로드됩니다.

## 빌드

```
dotnet build sts2-block-console.csproj
```
