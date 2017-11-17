import { Sprint } from "./sprint";

export class TaskBackLog{    
    constructor(public TaskId: number,public SprintId : number,public TaskName : string,
                public PersonId : number,public StartDate : Date,public EndDate : Date,public sprintBacklogs? : Sprint){}   
}