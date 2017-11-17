export class Burndown {
    constructor(
        public taskId: number,
        public actualDate: number,
        public expectedDate: number,
        public taskName:string,
        public projectName?:string,
        public storyName?:string,
        public projectId?:string
    ) { }
}