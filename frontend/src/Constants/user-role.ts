export const USER_ROLE = {
    PO: "PO",
    LEAD: "LEAD",
    SENIOR: "SENIOR",
    MID: "MID",
    JUNIOR: "JUNIOR"
} as const

export type userKeyRole = keyof typeof USER_ROLE;
export const RoleAdmin = [USER_ROLE.PO.toString(), USER_ROLE.LEAD.toString()];
export const RoleDev = [USER_ROLE.LEAD.toString(), USER_ROLE.SENIOR.toString(), USER_ROLE.MID.toString(), USER_ROLE.JUNIOR.toString()];