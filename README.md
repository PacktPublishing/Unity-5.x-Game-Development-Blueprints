#Unity 5.x Game Development Blueprints

This is the code repository for [Unity 5.x Game Development Blueprints](https://www.packtpub.com/game-development/unity-5x-game-development-blueprints?utm_source=github&utm_medium=repository&utm_campaign=9781785883118), published by Packt. It contains all the supporting project files necessary to work through the book from start to finish.

##Instructions and Navigation

we will work within the Unity 3D game engine, which you can download from [here](http://unity3d.com/unity/download/) The projects have been created using version 5.3.4f1 but should work with minimal changes for future versions.
For the sake of simplicity, we will assume that you are working on a Windows-powered computer. Although Unity allows you to code in either C#, Boo, or UnityScript, for this book, we will be using C#.

The commands and instructions will look like the following:
```
public void ButtonClicked()
{
	controller.Cash -= cost;
	switch (itemType)
	{
		case ItemType.ClickPower:
			controller.cashPerClick += increaseAmount;
		break;
		case ItemType.PerSecondIncrease:
			controller.CashPerSecond += increaseAmount;
		break;
	}
	qty++;
	qtyText.text = qty.ToString();
}
```


##Related Entity Framework Products:
* [Unity Game Development Blueprints](https://www.packtpub.com/game-development/unity-game-development-blueprints?utm_source=github&utm_medium=repository&utm_campaign=9781783553655)
* [Learning Unity 2D Game Development by Example](https://www.packtpub.com/game-development/learning-unity-2d-game-development-example?utm_source=github&utm_medium=repository&utm_campaign=9781783559046)
* [Getting Started with Unity 4 Scripting [Video]](https://www.packtpub.com/game-development/getting-started-unity-4-scripting-video?utm_source=github&utm_medium=repository&utm_campaign=9781849696128)
