package com.example.towerbuilderspring.model;

import org.springframework.validation.annotation.Validated;

import javax.persistence.*;
import javax.validation.constraints.NotNull;
import java.util.List;

@Entity
@Validated
@Table(name = "PaneColours")
public class PaneColours {

    @Id
    @Column(name = "ColourCode")
    private int colourCode;

    @NotNull
    @Column(name = "Colours", unique = true)
    private String colours;

    @OneToMany(fetch = FetchType.LAZY, mappedBy = "colours")
    private List<UserTowers> window_colours;

    public PaneColours() {};

    public PaneColours(int colourCode, String colours) {
        this.colourCode = colourCode;
        this.colours = colours;
    }

    public int getColourCode() {
        return colourCode;
    }

    public void setColourCode(int colourCode) {
        this.colourCode = colourCode;
    }

    public String getColours() {
        return colours;
    }

    public void setColours(String colours) {
        this.colours = colours;
    }

    @Override
    public String toString() {
        return "PaneColours{" +
                "colourCode=" + colourCode +
                ", colours='" + colours + '\'' +
                '}';
    }
}
