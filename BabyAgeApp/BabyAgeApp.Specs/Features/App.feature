Feature: App

Scenario: App can be started
	
	Then the app was started


Scenario: Refresh updates the UI
	We see this because the seconds changed after pressing the button

	Given the baby is born on '2020-01-01 00:00:01'
	And it is currently '2020-01-01 00:00:01'

	When it is currently '2020-01-01 00:00:10' 
	And a refresh happens

	Then the UI shows updated values
