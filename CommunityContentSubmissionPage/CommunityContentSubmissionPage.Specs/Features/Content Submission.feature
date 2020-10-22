Feature: Content Submission

Scenario: Title is set to SpecFlow Community Content Submission

	When the submission page is open
	Then the title of the page is 'SpecFlow Community Content Submission'


Scenario Outline: Input for submission is available

	When the submission page is open
	Then it is possible to enter a '<Input type>' with label '<Label>'

	Examples: 
		| Input type  | Label              |
		| Url         | Url to Content     |
		| Type        | Type of Content    |
		| Email       | Your Email address |
		| Description | Description        |


Scenario: Input from submission page is saved

	Assumption: There are no entries in the database

	Given the submission page is open
	And the filled out submission entry form
		| Label | Value                    |
		| Url   | https://www.specflow.org |
		| Type  | Website                  |

	When the submission entry form is submitted
	Then there is 'one' submission entry stored
