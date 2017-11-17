export class ProjectMaster {
    constructor(
        private name: string,
        private createdBy: number,
        private projectDescription: string,
        private technologyUsed: string,
        private modeOfOperation:string,
        private velocity:number,
        private rhythm:number,
        private approvedBy?:string,
        private reviewedBy?:string,
        private milestone?:string,
        private projectId?: number
    )
    { }
}