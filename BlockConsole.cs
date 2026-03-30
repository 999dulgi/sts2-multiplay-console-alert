using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Context;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Modding;
using MegaCrit.Sts2.Core.Nodes.Debug;
using System.Reflection;

namespace Sts2BlockConsole;

[ModInitializer("ModLoaded")]
public static class MultiplayConsoleAlert
{
    public static void ModLoaded()
    {
        var bc = new BlockConsole();
        var sceneTree = (SceneTree)Engine.GetMainLoop();
        sceneTree.Root.CallDeferred(Node.MethodName.AddChild, bc);
    }
}

[GlobalClass]
public partial class BlockConsole : Node
{
    private static BlockConsole? _instance;
    private Harmony? _harmony;
    private VBoxContainer? _container;

    public override void _Ready()
    {
        _instance = this;

        var canvasLayer = new CanvasLayer { Layer = 128 };

        _container = new VBoxContainer();
        _container.SetAnchorsPreset(Control.LayoutPreset.BottomLeft);
        _container.GrowVertical = Control.GrowDirection.Begin;
        _container.Position = new Vector2(16, -16);

        canvasLayer.AddChild(_container);
        AddChild(canvasLayer);

        _harmony = new Harmony("com.sts2.block-console");
        _harmony.PatchAll(Assembly.GetExecutingAssembly());
        GD.Print("[BlockConsole] Dev console mod loaded.");
    }

    public override void _ExitTree()
    {
        _instance = null;
        _harmony?.UnpatchAll("com.sts2.block-console");
    }

    public static void ShowNotification(string message)
    {
        if (_instance == null) return;
        _instance.CallDeferred(MethodName._SpawnNotification, message);
    }

    private void _SpawnNotification(string message)
    {
        if (_container == null) return;

        var label = new RichTextLabel
        {
            BbcodeEnabled = true,
            FitContent = true,
            AutowrapMode = TextServer.AutowrapMode.Off,
            Text = $"[color=#ffaa00][b][콘솔][/b][/color] {message}",
            SelfModulate = new Color(1, 1, 1, 1),
        };
        _container.AddChild(label);

        var tween = CreateTween();
        tween.TweenInterval(3.0);
        tween.TweenProperty(label, "self_modulate:a", 0f, 0.5);
        tween.TweenCallback(Callable.From(label.QueueFree));
    }
}

// 멀티플레이에서 다른 플레이어(호스트 등)가 명령 실행 시 알림 표시
[HarmonyPatch(typeof(NDevConsole), nameof(NDevConsole.ProcessNetCommand))]
static class Patch_NDevConsole_ProcessNetCommand
{
    static void Prefix(Player? player, string netCommand)
    {
        if (!LocalContext.IsMe(player))
            BlockConsole.ShowNotification($"{player}가 콘솔 명령을 실행했습니다: [color=#aaaaaa]{netCommand}[/color]");
    }
}
