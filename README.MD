# CoinView
ASP.NET Core application for tracking crypto trades

I built this because I wanted to. I know there are free services out there that provide way more features, but I simply wanted to make everything fit exacly my needs and to not rely on a third party at all. Using a web-aplication for all of this is a pretty stupid idea, this could have been achived with a regular desktop client as well and I can understand that nobody would want to run IIS and an SQL Server locally just for this. I simply chose this architecture because I wanted to get to know ASP.NET Core and Entity Framework.

Involved technologies:
- ASP.NET Core 2.0
- Razor Views (ASP MVC, not RazorPages)
- Entity Framework Core
- chart.js

CoinView features multiple users and provides a summary overview (using dummy data here obviously):
![Summary-Page](http://www.janislanger.com/games/CoinView/2018-02-24_CoinView_02.png)

This is the details page for a user with all buys, trades and creations:
![User-Page](http://www.janislanger.com/games/CoinView/2018-02-24_CoinView_03.png)
