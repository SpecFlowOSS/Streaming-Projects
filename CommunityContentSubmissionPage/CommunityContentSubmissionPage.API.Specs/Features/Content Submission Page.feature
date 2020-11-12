Feature: Content Submission Page

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