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

Scenario: Type should offer a list of unique entries

	When the submission page is open
	Then you can choose from the following Types:
		| Typename      |
		| Blog Posts    |
		| Books         |
		| Presentations |
		| Videos        |
		| Podcasts      |
		| Examples      |

Scenario: Input from submission page is saved

	Assumption: There are no entries in the database

	Given the submission page is open
	And the filled out submission entry form
		| Label | Value                    |
		| Url   | https://www.specflow.org |
		| Type  | Blog Posts               |

	When the submission entry form is submitted
	Then there is 'one' submission entry stored

Scenario: Entered values from submission page is saved

	Given the submission page is open
	And the filled out submission entry form
		| Label       | Value                    |
		| Url         | https://www.specflow.org |
		| Type        | Blog Posts               |
		| Email       | youremail@example.org    |
		| Description | Test Input               |

	When the submission entry form is submitted
	Then there is a submission entry stored with the following data:
		| Url                      | Type       | Email                 | Description |
		| https://www.specflow.org | Blog Posts | youremail@example.org | Test Input  |


Scenario: User does not accept the privacy policy should be an error when submitting
	
	Given the submission page is open
	And the submission entry form is filled
	But the privacy policy is not accepted

	When the submission entry form is submitted

	Then the submitting of data was not possible


Scenario: User agrees to privacy policy data should be submitted

	Given the submission page is open
	And the submission entry form is filled
	And the privacy policy is accepted

	When the submission entry form is submitted

	Then the submitting of data was possible