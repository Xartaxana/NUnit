Feature:EpamWebSite
	Cases to check EPAM website

Scenario Outline: Open last result of position search
	Given I search position for <KeyWord> in <Location> with remote checkbox
	When I open last search result
	Then <KeyWord> mentioned on a page

	Examples:
	| KeyWord    | Location      |
	| .NET       | Georgia       |
	| Python     | Kazakhstan    |
	| JavaScript | All Locations |

	Scenario: Download company overview file
	Given I download company overview file
	Then File with name 'EPAM_Corporate_Overview_Q4_EOY.pdf' downloaded

	Scenario Outline: Open article in the carousel
	Given I swipe first carousel in Insight page <n> times 
	When I open selected article from carousel
	Then The selected article has opened

	Examples:
	| n |
	| 2 |
	| 3 |
	| 4 |
	| 5 |

	Scenario Outline: Global search provides correct results
	Given I search in global search <KeyWord>
	Then All links in a list contain the word <KeyWord> in the text

	Examples:
	| KeyWord    |
	| Automation |
	| Cloud      |
	| BLOCKCHAIN |


