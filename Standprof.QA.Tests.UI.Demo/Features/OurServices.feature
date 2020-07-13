@ui @ourServices
Feature: Our Services
	In order to work with the company
	As a client
	I want to know about their services

@test_SP-3
Scenario: Clients can find a page about services
	Given I have opened the company Home page
	When I click Our Services
	Then the Our Services page should open
