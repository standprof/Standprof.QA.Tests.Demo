@ui @homePage
Feature: HomePage
	In order to know about Standprof
	As a client
	I want to see main information when I navigate to the web site

@test_SP-1
Scenario: Client sees title "Our top services"
	When I navigate to the Standprof web site
	Then the Home page should show a section with the title: "Our top services"

