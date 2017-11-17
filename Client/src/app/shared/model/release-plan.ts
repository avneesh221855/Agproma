export class ReleasePlan {
    public releaseName:string;
    public startDate: Date;
    public endDate: Date;
    public status: number;
    public projectId:number;
    public increment:number;
    public goal?: string;
    public releaseDate?:Date;
    public releasePlanId ?: number;
    public days?:number;   
 }