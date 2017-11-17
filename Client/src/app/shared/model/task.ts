export class Task{
    constructor(
        public  status:number,
        public sprintId:number,
        public taskName: string ,
        public personId:number ,
        public  startDate:Date,
        public  endDate:Date,
        public actualendDate?:Date,
        public taskId?:number
        ){}}
