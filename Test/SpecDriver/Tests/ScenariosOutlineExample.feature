Feature: Wykop Scenarios Outline
	As a normal person
	I want to do some actions on Wykop page
	To check out the news

@Wykop
Scenario Outline: All Wykop pages
	Given I'm on Wykop main page
	When I go to "<page>" page
	Then The page title is "<title>"
	 Examples:
	 | page        | title                                                                        |
	 | Wykop       | Wykop.pl - newsy, aktualności, gry, wiadomości, muzyka, ciekawostki, filmiki |
	 | Wykopalisko | Wykopalisko - Wykop.pl                                                       |
	 | Hity        | Hity - Wykop.pl                                                              |
	 | Mikroblog   | Mikroblog - Wykop.pl                                                         |