Feature: Duration Calculation

Scenario Outline: Age in Days Calculation

	Given the baby is born on '<Birthday>'
	When it is currently '<Now>'
	Then the baby is '<Days>' days old

Examples: 
	| Name                   | Birthday            | Now                 | Days |
	| One Second after Birth | 2020-01-01 00:00:01 | 2020-01-01 00:00:02 | 0    |
	| One Day after Birth    | 2020-01-01 00:00:01 | 2020-01-02 00:00:01 | 1    |
	| One Month after Birth  | 2020-01-01 00:00:01 | 2020-02-01 00:00:01 | 31   |

Scenario Outline: Age in Weeks Calculation

	Given the baby is born on '<Birthday>'
	When it is currently '<Now>'
	Then the baby is '<Weeks>' weeks old

Examples: 
	| Name                   | Birthday            | Now                 | Weeks |
	| One Second after Birth | 2020-01-01 00:00:01 | 2020-01-01 00:00:02 | 0     |
	| One Day after Birth    | 2020-01-01 00:00:01 | 2020-01-02 00:00:01 | 0     |
	| Seven Days after Birth | 2020-01-01 00:00:01 | 2020-01-08 00:00:01 | 1     |
	| One Month after Birth  | 2020-01-01 00:00:01 | 2020-02-01 00:00:01 | 4     |

Scenario Outline: Age in Month Calculation

	Given the baby is born on '<Birthday>'
	When it is currently '<Now>'
	Then the baby is '<Months>' months old

Examples: 
	| Name                   | Birthday            | Now                 | Months |
	| One Second after Birth | 2020-01-01 00:00:01 | 2020-01-01 00:00:02 | 0      |
	| One Day after Birth    | 2020-01-01 00:00:01 | 2020-01-02 00:00:01 | 0      |
	| One Month after Birth  | 2020-01-01 00:00:01 | 2020-02-01 00:00:01 | 1      |
	| One Year after Birth   | 2020-01-01 00:00:01 | 2021-01-01 00:00:01 | 12     |