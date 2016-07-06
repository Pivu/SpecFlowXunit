Feature: Wykop Scenarios
	As a normal person
	I want to do some actions on Wykop page
	To check out the news

@Wykop
Scenario: Main Wykop Page
	Given I'm on Wykop main page
	Then The page title is "Wykop.pl - newsy, aktualności, gry, wiadomości, muzyka, ciekawostki, filmiki"

Scenario: Mikroblog Wykop Page
	Given I'm on Wykop main page
	When I go to "Mikroblog" page
	Then The page title is "Mikroblog - Wykop.pl"

Scenario: Main Wykop Page Comments
	Given I'm on Wykop main page
	When I go to comments of the first post
	Then I should see some comments