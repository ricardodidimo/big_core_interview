

export interface datesResponse {
    startDate: string,
    endDate: string
}

export function getUTCDatesIntervals() : datesResponse {
    const now = new Date();
    
    const yesterday = new Date();
    yesterday.setDate(now.getDate() - 1);

    return {
        startDate: yesterday.toISOString().slice(0, 16),
        endDate: now.toISOString().slice(0, 16)
    }
}

export function fromLocalToUTC(localStartDate: string, localEndDate: string) : datesResponse {
    return {
        startDate: new Date(localStartDate).toISOString().slice(0, 16),
        endDate: new Date(localEndDate).toISOString().slice(0, 16)
    } 
}

export function getLocalDatesIntervals(): datesResponse {
    const now = new Date();
    const offsetMs = now.getTimezoneOffset() * 60000;
    const nowLocalDate = new Date(now.getTime() - offsetMs);
    
    const yesterday = new Date() ;
    yesterday.setDate(now.getDate() - 1);
    const yesterdayLocalDate = new Date(yesterday.getTime() - offsetMs);

    return {
        startDate: yesterdayLocalDate.toISOString().slice(0, 16),
        endDate: nowLocalDate.toISOString().slice(0, 16)
    }
}