export const USER_ROLE = {
    PO: "PO",
    LEAD: "LEAD",
    SENIOR: "LEAD",
    MID: "MID",
    JUNIOR: "JUNIOR"
} as const

export type userKeyRole = keyof typeof USER_ROLE;