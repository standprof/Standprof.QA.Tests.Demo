@ui @sendEmail
Feature: Send Email
	In order to contact the company
	As a client
	I want to send an email to the company

@test_SP-2
Scenario Outline: Client sends email
	Given I have opened the company Home page
	When I send an email with the following info:
	| name       | emailAddress            | details             |
	| John Smith | tester1@standprof.co.uk | Hello, how are you? |
	Then the page should show the "<ThankYouMessage>"
Examples: 
	| Test            | ThankYouMessage             |
	| Correct Message | Thank you for your message. |
	| Wrong Message   | Thank you for your messag.  |

