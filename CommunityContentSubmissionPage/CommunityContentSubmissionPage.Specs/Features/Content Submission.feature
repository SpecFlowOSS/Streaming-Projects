Feature: Content Submission

Scenario: Title is set to SpecFlow Community Content Submission

	When the submission page is open
	Then the title of the page is 'SpecFlow Community Content Submission'


Scenario: Url box for submission is available

	When the submission page is open
	Then it is possible to enter a 'URL' with label 'Url to Content'

