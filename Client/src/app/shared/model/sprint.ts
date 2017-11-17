export class Sprint {
    constructor(
        public projectId: number,
        public sprintName: string,
        public totalDays: number,
        public sprintGoal: string,
        public increment:number,
        public startDate: Date,
        public endDate: Date,
        public status: number,
        public plannedSize: number,
        public actualSize: number,
        public sprintId?: number
    ) { }
    setEndDate(endDate){
        this.endDate=endDate;
    }
}