package com.example.towerbuilderspring.model;

import javax.persistence.*;
import javax.validation.constraints.NotNull;

@Entity
public class UserModels {

    @EmbeddedId
    private UserModelId userModelId = new UserModelId();

    @NotNull
    private int building_xp;

    @NotNull
    private int height;

    @NotNull
    private int primaryColour;

    @NotNull
    private int secondaryColour;

    @NotNull
    private int modelGroup;


    public UserModels() {};

    public UserModels(UserModelId userModelId, int building_xp, int height,int primaryColour, int secondaryColour, int modelGroup) {
        this.userModelId = userModelId;
        this.building_xp = building_xp;
        this.height = height;
        this.primaryColour = primaryColour;
        this.secondaryColour = secondaryColour;
        this.modelGroup = modelGroup;
    }


    //    @Embeddable
//    public static class userModelId implements Serializable {
//        private static final long serialVersionUID = 1l;
//
//        private UUID userId;
//        private long modelCode;
//
//        public userModelId() {};
//
//        public userModelId(UUID userId, long modelCode) {
//            super();
//            this.userId = userId;
//            this.modelCode = modelCode;
//        }
//    }
}
