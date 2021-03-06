SELECT 
	--aliases from query below, both must batch
    Agency, AgencyCode, EmploymentCount, QuitCount, AttritionRate, AverageSalary, AverageService, PercentAdvancedDegrees, PercentFemale, Year,  
	[1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12], [13], [14], [15], [16], [17], [18], [19], [20], [21], [22], [23], [24], [25], [26], [27], [28], [29], [30], [31], [32], [33], [34], [35], [36], [37], [38], [39], [40], [41], [42], [43], [44], [45], [46], [47], [48], [49], [50], [51], [52], [53], [54], [55], [56], [57], [58], [59], [60], [61], [62], [63], [64], [65], [66], [67], [68], [69], [70], [71]
FROM  
	--query for all data
    (SELECT (SELECT AgencyName FROM Agency WHERE Agency.AgencyID = Survey.AgencyID) AS 'Agency', (SELECT AgencyCode FROM Agency WHERE Agency.AgencyID = Survey.AgencyID) AS 'AgencyCode', (SELECT EmployeeCount FROM Employment WHERE Employment.AgencyID = Survey.AgencyID) AS 'EmploymentCount', (SELECT QuitCount FROM Employment WHERE Employment.AgencyID = Survey.AgencyID) AS 'QuitCount', (SELECT AttritionRate FROM Employment WHERE Employment.AgencyID = Survey.AgencyID) AS 'AttritionRate', (SELECT AverageSalary FROM Employment WHERE Employment.AgencyID = Survey.AgencyID) AS 'AverageSalary', (SELECT AverageService FROM Employment WHERE Employment.AgencyID = Survey.AgencyID) AS 'AverageService', (SELECT Education FROM Employment WHERE Employment.AgencyID = Survey.AgencyID) AS 'PercentAdvancedDegrees', (SELECT Sex FROM Employment WHERE Employment.AgencyID = Survey.AgencyID) AS 'PercentFemale', (SELECT Year FROM Years WHERE Years.YearID= Survey.YearID) AS 'Year', QuestionNumber, ResponseValue FROM Survey)   
    AS Survey
PIVOT  
(  --row(s)
    MAX(ResponseValue)  
FOR
--pivoted column(s)   
[QuestionNumber]   
    IN ([1], [2], [3], [4], [5], [6], [7], [8], [9], [10], [11], [12], [13], [14], [15], [16], [17], [18], [19], [20], [21], [22], [23], [24], [25], [26], [27], [28], [29], [30], [31], [32], [33], [34], [35], [36], [37], [38], [39], [40], [41], [42], [43], [44], [45], [46], [47], [48], [49], [50], [51], [52], [53], [54], [55], [56], [57], [58], [59], [60], [61], [62], [63], [64], [65], [66], [67], [68], [69], [70], [71])  
) AS SurveyData
ORDER BY Agency 


