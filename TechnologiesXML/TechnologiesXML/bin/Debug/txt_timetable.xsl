<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="text" encoding="utf-8"/>
  <xsl:template match="/">
    <xsl:text>Номер недели: </xsl:text>
    <xsl:value-of select="//week/@num"/>
    <xsl:text>&#10;</xsl:text>
    <xsl:for-each select="//day">
      <xsl:if test="@num = 1">
        <xsl:text>Понедельник:&#10;</xsl:text>
      </xsl:if>
      <xsl:if test="@num = 2">
        <xsl:text>Вторник:&#10;</xsl:text>
      </xsl:if>
      <xsl:if test="@num = 3">
        <xsl:text>Среда:&#10;</xsl:text>
      </xsl:if>
      <xsl:if test="@num = 4">
        <xsl:text>Четверг:&#10;</xsl:text>
      </xsl:if>
      <xsl:if test="@num = 5">
        <xsl:text>Пятница:&#10;</xsl:text>
      </xsl:if>
      <xsl:for-each select="./lesson">
        <xsl:value-of select="@title"/> - <xsl:value-of select="./audience"/> - <xsl:value-of select="./teacher"/> - <xsl:value-of select="./time_begin"/> - <xsl:value-of select="./time_end"/> - <xsl:value-of select="./type"/>
        <xsl:text>&#10;</xsl:text> 
      </xsl:for-each>
    </xsl:for-each>
  </xsl:template>
</xsl:stylesheet>