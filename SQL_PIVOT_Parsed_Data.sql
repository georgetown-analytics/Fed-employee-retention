SELECT 
    AgencyID, YearID,  
	[1], [2], [3], [4], [5], [6]
FROM  
    (SELECT AgencyID, YearID, QuestionNumber, ResponseValue FROM Survey)   
    AS Survey
PIVOT  
(  
    MAX(ResponseValue)  
FOR   
[QuestionNumber]   
    IN ([1],[2], [3], [4], [5], [6])  
) AS SurveyData
ORDER BY AgencyID 


