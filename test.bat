@echo off

Packages\xunit.runner.console.2.1.0\tools\xunit.console ^
	CarPark.Facts\bin\Debug\CarPark.Facts.dll ^
	-parallel all ^
	-html Result.html ^
	-nologo  
@echo on 